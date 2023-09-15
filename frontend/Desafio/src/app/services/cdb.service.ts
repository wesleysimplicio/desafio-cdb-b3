import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

import { Config } from "./config";

import { RetornoSimulacao } from '@app/models/RetornoSimulacao';
import { DadosSimulacao } from '@app/models/DadosSimulacao';

@Injectable({
  providedIn: 'root'
})
export class CdbService {

  constructor(private httpClient: HttpClient) { }

  public obterDadosDaSimulacao(data: DadosSimulacao): Observable<RetornoSimulacao> {
    const baseUrl = `${Config.api}`;
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    };
    return this.httpClient.post<RetornoSimulacao>(baseUrl, data, httpOptions).pipe(take(1));
  }
}
