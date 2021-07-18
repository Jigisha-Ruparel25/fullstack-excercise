import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class Emailservice {

  constructor(
    private http: HttpClient
  ) {}

  getFiles(): Observable<any> {
    return this.http.get(`${environment.baseUrl}/Mail`);
  }

  sendEmail(body: any): Observable<any> {
    return this.http.post(`${environment.baseUrl}/Mail`, body);
  }
}
