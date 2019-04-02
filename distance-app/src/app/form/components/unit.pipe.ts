import { Pipe, PipeTransform } from '@angular/core';
import { Unit } from '../models/model';

@Pipe({name: 'unit'})
export class UnitPipe implements PipeTransform {
    transform(value: Unit): string {
        switch(value) {
            case Unit.meters:
                return 'm';
            case Unit.miles:
                return 'mi';
            default:
                return value;
        }
    }
}
