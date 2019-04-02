import { Component, ViewChild } from '@angular/core';
import { Coordinates } from '../models/model';
import { DAppDistanceService } from '../services/distance.service';
import { take } from 'rxjs/operators';
import { NgForm, NgModel } from '@angular/forms';
import { DAppCoordinateComponent } from './coordinate.component';

@Component({
    selector: 'dapp-form',
    templateUrl: './form.component.html',
})
export class DAppFormComponent {
    @ViewChild('dappForm')
    formRef: NgForm;

    @ViewChild('from')
    from: NgModel;

    @ViewChild('to')
    to: NgModel;

    model: Coordinates = {
        from: { latitude: 0.000000, longitude: 0.000000 },
        to: { latitude: 0.000000, longitude: 0.000000 }
    }

    result = null;
    errorText = '';

    constructor(private _distanceService: DAppDistanceService) { }

    onSubmit() {
        const formValid = this.formRef.valid;

        this.toggleValidationErrors(!formValid);

        if (formValid) {
            this._distanceService
                .calculate(this.model)
                .pipe(take(1))
                .subscribe(
                    n => {
                        this.result = n;
                    },
                    err => this.errorText = err
                );
        }
    }

    private toggleValidationErrors(show: boolean) {
        const fromCoordinate = <DAppCoordinateComponent>this.from.valueAccessor;
        const toCoordinate = <DAppCoordinateComponent>this.to.valueAccessor;

        fromCoordinate.toggleErrorVisibility(show)
        toCoordinate.toggleErrorVisibility(show);
    }
}
