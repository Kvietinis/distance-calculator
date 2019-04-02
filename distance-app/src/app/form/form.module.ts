import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { DAppFormComponent } from './components/form.component';
import { DAppDistanceService } from './services/distance.service';
import { DAppCoordinateComponent } from './components/coordinate.component';
import { UnitPipe } from './components/unit.pipe';

@NgModule({
    declarations: [ DAppFormComponent, DAppCoordinateComponent, UnitPipe ],
    imports: [ ReactiveFormsModule, FormsModule, BrowserModule ],
    providers: [ DAppDistanceService ],
    exports: [ DAppFormComponent ]
})
export class DAppFormModule { }
