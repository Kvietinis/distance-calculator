import { TestBed, getTestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { DAppHttpClient } from './http.client';

interface SomeObject {
    p1: string;
    p2: string;
}

describe('DAppHttpClient', () => {
    let injector: TestBed;
    let client: DAppHttpClient;
    let http: HttpTestingController;

    beforeEach(() => {
        TestBed.configureTestingModule({
            imports: [ HttpClientTestingModule ],
            providers: [
                DAppHttpClient,
                { provide: 'BASE_URL', useValue: 'baseUrl'}
            ]
        });

        injector = getTestBed();
        client = injector.get(DAppHttpClient);
        http = injector.get(HttpTestingController);
    });
    afterEach(() => {
        http.verify();
    });

    test('should return response on GET', (done) => {
        const someObj = {
            p1: 'property1',
            p2: 'property2'
        }

        client.get<SomeObject>('someUrl').subscribe(o => {
            expect(o.p1).toEqual('property1');
            expect(o.p2).toEqual('property2');
            done();
        });

        const req = http.expectOne('baseUrl/someUrl');

        expect(req.request.method).toBe('GET');

        req.flush(someObj);
    });

    test('should return response on POST', (done) => {
        const someObj = {
            p1: 'property1',
            p2: 'property2'
        }

        client.post<SomeObject>('someUrl').subscribe(o => {
            expect(o.p1).toEqual('property1');
            expect(o.p2).toEqual('property2');
            done();
        });

        const req = http.expectOne('baseUrl/someUrl');

        expect(req.request.method).toBe('POST');

        req.flush(someObj);
    });

    test('shoud return error', (done) => {
        client.get<SomeObject>('someUrl')
            .subscribe(
                () => {},
                err => {
                    expect(err).toContain('Client error');
                    done();
                }
            );

        const req = http.expectOne('baseUrl/someUrl');
        req.flush({ message: 'some error' }, { status: 400, statusText: 'Client error' });
    });
    test('shoud return error', (done) => {
        client.get<SomeObject>('someUrl')
            .subscribe(
                () => {},
                err => {
                    expect(err).toBe('Oops. Something went wrong. Contact support.');
                    done();
                }
            );

        const req = http.expectOne('baseUrl/someUrl');
        req.flush({ message: 'some error' }, { status: 500, statusText: 'Server error' });
    });
});
