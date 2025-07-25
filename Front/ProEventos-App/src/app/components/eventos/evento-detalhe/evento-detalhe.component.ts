import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {

  evento = {} as Evento;
  form!: FormGroup;
  locale = 'pt-br';
  estadoSalvar = 'post';

  get f(): any {
    return this.form.controls;
  }

  get bsConfig(): any {
    return {
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm:ss a',
      ContainerClass: 'theme-default',
      showWeekNumbers: false
    };
  }

  constructor(private fb: FormBuilder,
              private localeService: BsLocaleService,
              private router: ActivatedRoute,
              private eventoService: EventoService,
              private spinner: NgxSpinnerService,
              private toastr: ToastrService)
    {
      this.localeService.use(this.locale);
  }

  public carregarEvento(): void {
    const eventoIdparam = this.router.snapshot.paramMap.get('id');

    if (eventoIdparam != null) {
      this.spinner.show();
      this.estadoSalvar = 'put';

      this.eventoService.getEventoById(+eventoIdparam).subscribe(
        (evento: Evento) => {
          this.evento = {...evento};
          this.form.patchValue(this.evento);
        },
         (error: any) => {
          this.spinner.hide();
          this.toastr.error('Erro ao tentar carregar evento.', 'Erro!');
          console.log(error);
        },
        () => this.spinner.hide()
      );
    }
  }

  ngOnInit(): void {
    this.carregarEvento();
    this.validation();
  }

  public validation(): void{
    this.form = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(500)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imageURL: ['', Validators.required],
    });
  }

  public onsubmit(): void {
    if (this.form.invalid) {
      return;
    }
  }

   public resetForm(event: any): void{
    event.preventDefault();
    this.form.reset();
  }

  public cssValidator(formControl: FormControl): any {
    return { 'is-invalid': formControl.errors && formControl.touched }
  }

  public salvarAlteracao(): void {
    this.spinner.show();

    if(this.form.valid) {

      this.evento = (this.estadoSalvar === 'post')
        ? {...this.form.value}
        : {id: this.evento.id, ...this.form.value};

      this.eventoService[this.estadoSalvar](this.evento).subscribe(
        () => this.toastr.success(`Evento salvo com sucesso.`,'sucesso!'),
        (error: any) => {
          console.log(error);
          this.spinner.hide();
          this.toastr.error('Erro ao salvar o evento.','Erro!');
        },
        () => this.spinner.hide()
      )
    }
  }
}
