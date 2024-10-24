import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { environment } from '../environments/environment';
import { createClubEventVM } from '../models/createClubEventVM';
import { BaseService } from './base.service';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

const apiUrl = `${environment.apiUrl}/clubs`;
const sizeDefault = 10;

@Injectable({
  providedIn: 'root'
})
export class GameClubService extends BaseService{

  constructor(private http: HttpClient) {
    super();
   }

  // create club
  addClub(createClubVM: any): Observable<any> {
    return this.http.post<any>(apiUrl, createClubVM, httpOptions).pipe(
      tap((data: any) => console.log('Added', data)),
      catchError(this.handleError)
    );
  }

  // get all or search clubs
  getClubs(queryParams: any, page: number = 0, size: number = sizeDefault): Observable<any> {

    let params: any = {
      pageNumber: page,
      pageSize: size
    };

    if(queryParams.id){
      params.id = queryParams.id;
    }

    if(queryParams.text){
      params.text = queryParams.text;
    }

    if(queryParams.name){
      params.name = queryParams.name;
    }

    if(queryParams.description){
      params.description = queryParams.description;
    }

    return this.http.get<any>(apiUrl, { params: params })
      .pipe(
        tap(data => console.log('Fetch Club', data)),
        catchError(this.handleError)
      );
  }

  // create club
  addClubEvent(clubId: number, createClubEvent: createClubEventVM): Observable<any> {
    const url = `${apiUrl}/${clubId}/events`;
    return this.http.post<any>(url, createClubEvent, httpOptions).pipe(
      tap((data: any) => console.log('Added', data)),
      catchError(this.handleError)
    );
  }

  // get club events
  getClubEvents(clubId: number): Observable<any> {
    const url = `${apiUrl}/${clubId}/events`;
    return this.http.get<any>(url).pipe(
      tap(data => console.log('Fetched Events', data)),
      catchError(this.handleError)
    );
  }
}
