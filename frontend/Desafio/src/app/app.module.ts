import { HttpClientModule } from '@angular/common/http';
import { NgModule, LOCALE_ID } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import ptBr from '@angular/common/locales/pt';
import {CurrencyPipe, registerLocaleData} from '@angular/common';

import { AppComponent } from './app.component';
import { SimuladorAplicacaoComponent } from './components/simulador-aplicacao/simulador-aplicacao.component';


registerLocaleData(ptBr);

@NgModule({
  declarations: [
    AppComponent,
    SimuladorAplicacaoComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ReactiveFormsModule
  ],
  providers: [
    {provide: LOCALE_ID, useValue: 'pt' },
    CurrencyPipe
  ],
  schemas: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
