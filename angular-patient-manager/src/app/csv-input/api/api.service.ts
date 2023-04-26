import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable, throwError, of } from 'rxjs';
import { catchError, map, retry, tap } from 'rxjs/operators';
import { PatientRecord } from '../csv-input.component';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  public BASE_URL= 'https://localhost:7074/api/patientrecords';
  constructor(private http:HttpClient) { }

  public postPatientRecords(patientRecords: PatientRecord[]): Observable<PatientRecord[]>{
    var requestBody = 
    {
      'patientRecords': patientRecords
    }
    
    return this.http.post<PatientRecord[]>(this.BASE_URL, patientRecords);
  };
}