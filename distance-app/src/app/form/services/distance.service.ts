import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Coordinates, Distance } from '../models/model';
import { DAppHttpClient } from '../../clients/http.client';

@Injectable()
export class DAppDistanceService {
    constructor(private _httpClient: DAppHttpClient) { }

    calculate(coordPair: Coordinates): Observable<Distance> {
        const url = 'api/measure/distance';

        return this._httpClient.post<Distance>(url, coordPair);
    }
}