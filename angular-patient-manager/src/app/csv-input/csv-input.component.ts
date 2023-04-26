import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { CSV_REGEX } from '../constants/fileTypePatterns'
import { Papa } from 'ngx-papaparse';
import { ApiService } from './api/api.service';
import { HttpClient } from '@angular/common/http';
import { throwToolbarMixedModesError } from '@angular/material/toolbar';

export interface PatientRecord {
  id: number;
  firstName: string;
  lastName: string;
  birthDate: Date;
  gender: string;
}

@Component({
  selector: 'app-csv-input',
  templateUrl: './csv-input.component.html',
  styleUrls: ['./csv-input.component.css']
})
export class CSVInputComponent implements OnInit {
  @Output() fileUpload = new EventEmitter();
  fileData: any;
  fileName: string;
  fileTypeValid: boolean = true;
  patientRecords: PatientRecord[] = [];
  btnVal = 'Upload CSV';
  fileSubmitted = false;
  uploadedPatientRecords: string;
  uploadFailed = false;
  errorTitle: string;
  uploadTimestamp: string;
  validCSV = true;
  csvErrors: string[] = [];
  
  constructor(private papa: Papa, private api:ApiService) { }

  ngOnInit() {}

  fileChange(uploadEvent) {
    this.fileTypeValid = true;
    const { files } = uploadEvent.target;
    if (files && files.length > 0) {
      const file: File = files.item(0);
      this.fileName = file.name;
      if (!CSV_REGEX.test(file.name)) {
        return false;
      }
      const reader: FileReader = new FileReader();
      reader.readAsText(file);
      reader.onload = () => {
        this.fileData = reader.result;
      };
    }
  }

  uploadFormSubmit() {
    if(this.fileSubmitted == true)
    {
      location.reload();
    } else {

      if (!CSV_REGEX.test(this.fileName)) {
        this.fileTypeValid = false;
        return false;
      }

      this.parseCsvFileAndMakeRequests(this.fileData);
    }
  }

  async parseCsvFileAndMakeRequests(file: File): Promise<void> {
    const csvData = await this.parseCsvFile(file);
    var counter = 2;

    for (const row of csvData) {
      var tempPatientRecord: PatientRecord = {
        id: null,
        firstName: '',
        lastName: '',
        birthDate: null,
        gender: ''
      };

      if(row.Id == ''){
        tempPatientRecord.id = -1;
      }else{
        tempPatientRecord.id = row.Id;
      }
      tempPatientRecord.firstName = row.FirstName,
      tempPatientRecord.lastName = row.LastName,
      tempPatientRecord.birthDate = new Date(row.Birthday),
      tempPatientRecord.gender = row.Gender[0].toUpperCase();

      // validate csv data fields
      this.validCSV = this.validateCSVInput(tempPatientRecord, counter);

      if(this.validCSV)
      {
        this.patientRecords.push(tempPatientRecord);
      }

      counter++;
    }
      counter = 2;
      this.btnVal = 'Loading File . . .'

      this.delay(1000).then(any => {
        const now = new Date();
        this.uploadTimestamp = now.toLocaleString();

        // If csv data fields are valid do the following:
        if(this.patientRecords.length == csvData.length)
        {

          this.api.postPatientRecords(this.patientRecords)
          .subscribe((customList: PatientRecord[])=> {
            this.uploadedPatientRecords = 'Successfully Uploaded ' + customList.length.toString() + ' Patient Records';
            this.uploadFailed = false;

            this.btnVal = 'File submitted - click here to refresh page and table data ';
            this.fileSubmitted = true; 
            this.fileData = this.uploadedPatientRecords;
          },
          (err)=>{
            console.log('Error while calling postPatientRecords method  ' + err);
            this.uploadFailed = true;
            this.fileSubmitted == false;
            this.errorTitle = err.error.title;
            this.btnVal = 'Upload CSV'
          });

          this.patientRecords = []; 
        } else {
          this.uploadFailed = true;
          this.fileSubmitted == false;
          this.errorTitle = 'CSV contains invalid patient record data';
          this.btnVal = 'Upload CSV'
          this.patientRecords = []; 

          var nHTML = '';
          this.csvErrors.forEach(element => {
            nHTML += '<li>' + element + '</li>';
          });

          document.getElementById("csv-error-list").innerHTML = '<ul>' + nHTML + '</ul>';
        }
    });
  }

  async delay(ms: number) {
    await new Promise<void>(resolve => setTimeout(()=>resolve(), ms)).then(()=>console.log("fired"));
  }

  private validateCSVInput(patientRecord: PatientRecord, lineNum: any){
    if(!isFinite(patientRecord.id))
    {
      this.csvErrors.push('Line ' + lineNum.toString() + ': invalid patient record id \n');
      return false;
    }
    if(!Boolean(patientRecord.firstName))
    {
      this.csvErrors.push('Line ' + lineNum.toString() + ': invalid patient record first name \n');
      return false;
    }
    if(!Boolean(patientRecord.lastName))
    {
      this.csvErrors.push('Line ' + lineNum.toString() + ': invalid patient record last name \n');
      return false;
    }
    if(isNaN(patientRecord.birthDate.getTime()))
    {
      this.csvErrors.push('Line ' + lineNum.toString() + ': invalid patient record birthdate \n');
      return false;
    }
    if(patientRecord.gender != 'M')
    {
      if(patientRecord.gender != 'F')
      {
        this.csvErrors.push('Line ' + lineNum.toString() + ': invalid patient record gender \n');
        return false;
      }
    }

    return true
  }

  private parseCsvFile(file: File): Promise<any[]> {
    return new Promise<any[]>((resolve, reject) => {
      const options = {
        header: true,
        delimiter: ',',
        skipEmptyLines: true,
        complete: (results) => {
          resolve(results.data);
        },
        error: (error) => {
          reject(error);
        }
      };
      this.papa.parse(file, options);
    });
  }
}