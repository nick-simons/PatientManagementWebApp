import { AfterViewInit, Component, OnInit, ViewChild } from "@angular/core";
import { MatPaginator } from "@angular/material/paginator";
import { MatSort } from "@angular/material/sort";
import { MatTableDataSource } from "@angular/material/table";
import { ApiService } from './api/api.service';
import { HttpClient } from '@angular/common/http';

export interface PatientRecord {
  id: number;
  firstName: string;
  lastName: string;
  birthDate: Date;
  gender: string;
}

@Component({
  selector: "app-data-table",
  templateUrl: "./data-table.component.html",
  styleUrls: ["./data-table.component.css"],
})
export class DataTableComponent implements OnInit {
  displayedColumns: string[] = ["id", "firstName", "lastName", "birthDate", "gender"];
  dataSource:MatTableDataSource<any>;
  @ViewChild(MatPaginator, {static: false}) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  
  constructor(private api:ApiService){}
  
  public ngOnInit(): void {
    this.dataSource=new MatTableDataSource([]);
    this.loadInitialData();
  }

  public loadInitialData(){
    this.api.getAllPatientRecords()
      .subscribe((customList: PatientRecord[])=> {
        this.dataSource = new MatTableDataSource<any>(customList);
        this.dataSource.sort = this.sort;
        this.dataSource.paginator = this.paginator;
      },(err)=>{
        console.log('Error while calling getAllPatientRecords method  ' + err);
      })
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }

  onRowClicked(row: Object) {
    console.log("Row clicked: ", row);
  }
}
