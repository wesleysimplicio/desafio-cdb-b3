import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { CurrencyPipe } from '@angular/common';

import { RetornoSimulacao } from '@app/models/RetornoSimulacao';
import { DadosSimulacao } from '@app/models/DadosSimulacao';
import { CdbService } from '@app/services/cdb.service';

@Component({
  selector: 'app-simulador-aplicacao',
  templateUrl: './simulador-aplicacao.component.html',
  styleUrls: ['./simulador-aplicacao.component.scss']
})
export class SimuladorAplicacaoComponent implements OnInit {

  form!: FormGroup;
  retornoSimulacao = this.limpaResultado();
  enviandoDados = false;
  valorInicial = 'R$ 0,00';
  prazoInicial = '2';
  erroEnvio = false;

  constructor(
    private fb: FormBuilder,
    private cdbService: CdbService,
    private currencyPipe: CurrencyPipe
  ) { }

  ngOnInit(): void {
    this.configuraCampos();
    this.monitoraAlteracaoDeValores();
  }

  public get f(): any { return this.form.controls; }

  public cssValidator(field: FormControl): any {
    return { 'is-invalid': field.errors && field.touched };
  }

  public enviaDados(): void {
    const valorSemFormatacao = parseFloat(this.retornaSomenteDigitos(this.form.value.valor)) / 100.0;
    const dadosParaEnvio = {
      valor: valorSemFormatacao,
      prazo: +this.form.value.prazo
    } as DadosSimulacao;
    this.enviandoDados = true;
    this.retornoSimulacao = this.limpaResultado();

    this.cdbService.obterDadosDaSimulacao(dadosParaEnvio).subscribe({
      next: (response: RetornoSimulacao) => {
        this.erroEnvio = false;
        this.retornoSimulacao = { ...response };
      },
      error: (error: Error) => {
        this.erroEnvio = true;
        console.error(JSON.stringify(error));
      },
    }).add(() => this.enviandoDados = false);
  }

  public limpaDadosGeral() : void {
    this.erroEnvio = false;
    this.form.reset();
    this.form.patchValue({
      valor: this.valorInicial,
      prazo: this.prazoInicial
    });
    this.retornoSimulacao = this.limpaResultado();
  }

  private configuraCampos(): void {
    this.form = this.fb.group({
      valor: [this.valorInicial, [Validators.required, Validators.min(0.01), Validators.max(999_999_999_999.99)]],
      prazo: [this.prazoInicial, [Validators.required, Validators.min(2), Validators.max(120)]]
    });
  }

  private monitoraAlteracaoDeValores(): void {
    this.form.valueChanges.subscribe(f => {
      if (f.valor) {
        const newValue = parseFloat(this.retornaSomenteDigitos(f.valor)) / 100.0;
        this.form.patchValue({
          valor: this.currencyPipe.transform(newValue, 'BRL', 'symbol', '1.2-2')
        }, { emitEvent: false });
      }
      if (f.prazo) {
        this.form.patchValue({
          prazo: this.retornaSomenteDigitos(f.prazo)
        }, { emitEvent: false });
      }
    });
  }

  private retornaSomenteDigitos(valor: string): string {
    return valor.replace(/\D/g, '').replace(/^0+/, '') || '0';
  }

  private limpaResultado(): RetornoSimulacao {
    return {
      valor_bruto: 0.00,
      valor_liquido: 0.00
    } as RetornoSimulacao;
  }
}
