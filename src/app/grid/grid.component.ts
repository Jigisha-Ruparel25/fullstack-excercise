import { Component, OnInit, ViewChild } from '@angular/core';
import { NgbModal, NgbModalConfig, NgbModalRef } from '@ng-bootstrap/ng-bootstrap';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Emailservice } from '../service/emailservice.service';
import { NgxUiLoaderService } from 'ngx-ui-loader';
import { AlertsService } from 'angular-alert-module';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss']
})
export class GridComponent implements OnInit {

  fileList: any[] = [];
  @ViewChild('agGrid') agGrid: any;
  columnDefs = [{ field: 'fileName' }, { field: 'createdAt' , cellRenderer: (data: any) => {
    return data.value ? (new Date(data.value)).toLocaleDateString() : '';
}}, { field: 'fileType'}];
  gridApi: any;
  gridColumnApi: any;
  rowData = [];
  defaultColDef = {
    flex: 1,
    minWidth: 150,
    filter: true,
  };
  emailForm = new FormGroup({
    email: new FormControl('', [Validators.required, Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')]),
    subject: new FormControl('', [Validators.required]),
    body: new FormControl(''),
    cc: new FormControl('', [Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')]),
    bcc: new FormControl('', [Validators.pattern('^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$')])
  });
  submitted = false;

  selectedAll = false;
  isCSV = false;
  isPDF = false;
  isDOC = false;

  modalReference!: NgbModalRef;

  constructor(config: NgbModalConfig, private modalService: NgbModal, private emailService: Emailservice,
              private ngxUiLoaderService: NgxUiLoaderService, private alerts: AlertsService) { }

  ngOnInit(): void {
    this.getFileList();
  }

  open(content: any): void {
    this.modalReference = this.modalService.open(content);
  }
  onGridReady(params: any): void {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
  }
  get f(): any {
    return this.emailForm.controls;
  }
  save(): void{
    this.submitted = true;
    if (this.emailForm.invalid) {
      return;
    }
    this.sendFiles();
  }

  submitEmail(): any {

  }

  getFileList(): any {
   this.emailService.getFiles().subscribe((response: any) => {
    this.rowData = response;
   });
  }

  onChangeSelectAll(event: any): void {
    this.selectedAll = !this.selectedAll;
    this.isCSV = this.selectedAll;
    this.isDOC = this.selectedAll;
    this.isPDF = this.selectedAll;
  }

  onChangeEvent(event: any, element: any): void {
    element = !element;
  }

  sendFiles(): any {
    this.ngxUiLoaderService.start();
    const requestBody = {
      Body: this.emailForm.value.body,
      Cc: this.emailForm.value.Cc,
      Bcc: this.emailForm.value.bcc,
      Subject: this.emailForm.value.subject,
      To: this.emailForm.value.email,
      IsPdf: this.isPDF,
      IsCsv: this.isCSV,
      IsDoc: this.isDOC
    };

    this.emailService.sendEmail(requestBody).subscribe((response) => {
      this.ngxUiLoaderService.stop();
      this.alerts.setMessage('Configurations saved successfully!', 'success');
      this.modalReference.close();
      console.log(response);
    });
  }
}
