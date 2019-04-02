import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { DAppHttpClient } from './clients/http.client';
import { DAppFormModule } from './form/form.module';

@NgModule({
    declarations: [ AppComponent, HomeComponent ],
    imports: [
        HttpClientModule,
        DAppFormModule
    ],
    providers: [
        DAppHttpClient
    ],
    bootstrap: [ AppComponent ]
})
export class AppModule {}
