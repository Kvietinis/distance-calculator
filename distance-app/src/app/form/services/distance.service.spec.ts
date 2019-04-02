import { TestBed, getTestBed } from '@angular/core/testing';
import { Observable, Subject } from 'rxjs';
import { DAppHttpClient } from 'src/app/clients/http.client';
import { DAppDistanceService } from './distance.service';

const subjectConst = new Subject<any>();

class HttpClientMock {
    subject = subjectConst

    get<T>(): Observable<T> {
        return this.subject;
    }

    post<T>(): Observable<T> {
        return this.subject;
    }
}
describe('distance service', () => {
    let injector: TestBed;
    let service: DAppDistanceService;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [  ],
            providers: [
                DAppDistanceService,
                { provide: DAppHttpClient, useClass: HttpClientMock }
            ]
        });

        injector = getTestBed();
        service = injector.get(DAppDistanceService);
    });

    test('should return distance', (done) => {
        const coords = {
            from: { 
                latitude: 10,
                longitude: 10.1
            },
            to: {
                latitude: 11.1,
                longitude: 11
            }
        }
        service.calculate(coords).subscribe(d => {
            expect(d.value).toBe(100);
            expect(d.units).toBe('m');
            done();
        });

        subjectConst.next({ value: 100, units: 'm' });
    });
});
