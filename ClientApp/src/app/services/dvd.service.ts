import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, of } from 'rxjs';

export interface Dvd {
  id: number;
  title: string;
  price: number;
  releaseDate: Date;
  imageUrl:string;
}
@Injectable({
  providedIn: 'root'
})
export class DvdService {
  private apiUrl = 'https://localhost:7294/api/Dvd/get-all';
  constructor(private http: HttpClient) {}

  getAllDvds(): Observable<Dvd[]> {
    return this.http.get<Dvd[]>(this.apiUrl).pipe(
      catchError(error => {
        console.error('Error fetching data from API', error);
        return of([]);  // Return an empty array on error
      })
    );
  }
}
