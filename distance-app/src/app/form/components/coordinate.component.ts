import { Component, Input, forwardRef, ViewChild, ElementRef, } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NG_VALIDATORS, AbstractControl, Validator } from '@angular/forms';
import { Coordinate } from '../models/model';

@Component({
    selector: 'dapp-coordinate',
    templateUrl: './coordinate.component.html',
    styleUrls: ['./coordinate.component.css'],
    providers: [
        {
            provide: NG_VALUE_ACCESSOR,
            useExisting: forwardRef(() => DAppCoordinateComponent),
            multi: true
        },
        {
            provide: NG_VALIDATORS,
            useExisting: forwardRef(() => DAppCoordinateComponent),
            multi: true
        }
    ]
})
export class DAppCoordinateComponent implements ControlValueAccessor, Validator {
    @ViewChild('fieldset')
    fieldsetElemRef: ElementRef;
    
    @Input()
    disabled = false;

    @Input()
    get value(): Coordinate {
        return this._value;
    }
    set value(coord: Coordinate) {
        if (!this.disabled) {
            this._value = coord;
            this.longitude = coord ? coord.longitude : null;
            this.latitude = coord ? coord.latitude : null;
            this.onChange(coord);
            this.onTouched();
        }
    }

    longitude = 0.000000;
    latitude = 0.000000;
    showLongitudeError = false;
    showLatitudeError = false;

    private _value = { latitude: this.latitude, longitude: this.longitude };
    private _isLatitudeValid = true;
    private _isLongitudeValid = true;

    readonly validationText = 'Valid number required: e.g. 00.000000';
    readonly pattern = '[-+]?[0-9]*\.?[0-9]+$';

    onLatitudeInput(latitude: number) {
        if (this.value) {
            this.value = {...this.value, latitude: Number(latitude) };
        }
    }

    onLongitudeInput(longitude: number) {
        if (this.value) {
            this.value = {...this.value, longitude: Number(longitude) };
        }
    }

    onChange = (_coord: Coordinate) => { };
    onTouched = () => { };

    writeValue(coord: Coordinate) {
        if (coord) {
            this.value = coord;
        }
    }

    registerOnChange(fn: (coord: Coordinate) => void) {
        this.onChange = fn;
    }

    registerOnTouched(fn: () => void) {
        this.onTouched = fn;
    }

    setDisabledState(isDisabled: boolean) {
        this.disabled = isDisabled;
        this.fieldsetElemRef.nativeElement.disabled = isDisabled
    }

    validate(control: AbstractControl) {
        const re = new RegExp(this.pattern);

        this._isLatitudeValid = re.test(this.latitude != null ? this.latitude.toString() : '');
        this._isLongitudeValid = re.test(this.longitude != null ? this.longitude.toString() : '');

        return !this._isLatitudeValid || !this._isLongitudeValid ? { 'coordinate': { value: control.value } } : null;
    }

    toggleErrorVisibility(show: boolean) {
        this.showLongitudeError = !this._isLongitudeValid && show;
        this.showLatitudeError = !this._isLatitudeValid && show;
    }
}
