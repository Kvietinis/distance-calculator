import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable, Inject } from '@angular/core';
import { Observable, throwError, of } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class DAppHttpClient {
    constructor(private _http: HttpClient, @Inject('BASE_URL') private _baseUrl: string) { }

    get<T>(url: string): Observable<T> {
        return this._http
            .get<T>(`${this._baseUrl}${url}`)
            .pipe(
                catchError((e: HttpErrorResponse) => {
                    return throwError(this.handleError(e));
                })
            );
    }

    post<T>(url: string, body?: any): Observable<T> {
        return this._http
            .post<T>(`${this._baseUrl}${url}`, body)
            .pipe(
                catchError((e: HttpErrorResponse) => {
                    return throwError(this.handleError(e));
                })
            );
    }

    private handleError(error: HttpErrorResponse): string {
        // client error, show the text message from server
        if (error.status >= 400 && error.status < 500) {
            return error.message;
        }

        // server error, lets show some general message
        return 'Oops. Something went wrong. Contact support.';
    }
}
