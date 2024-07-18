import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { EventoService } from '../../services/evento.service';
import { Evento } from '../../models/Evento';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
  //,providers: [EventoService]
})
export class EventosComponent implements OnInit {
  modalRef?: BsModalRef;
  public events: Evento[] = [];
  public filteredEvents: Evento[] = [];

  public title = "Eventos";
  public widthImg = 120;
  public marginImg = 2;
  public showImg = true;
  private listFiltered: string = '';

  constructor(
      private eventoService: EventoService,
      private modalService: BsModalService,
      private toastrService: ToastrService,
      private spinner: NgxSpinnerService
    ) { }

  public get listFilter(): string {
    return this.listFiltered;
  }

  public set listFilter(value: string) {
    this.listFiltered = value;
    this.filteredEvents = this.listFilter ? this.filterEvents(this.listFilter) : this.events;
  }

  public ngOnInit(): void {
    this.spinner.show();

    this.getEventos();
  }

  public showImage(): void{
    this.showImg = !this.showImg;
  }

  public getEventos(): void {
    this.eventoService.getEventos().subscribe({
      next: (eventos: Evento[]) => {
        this.events = this.filteredEvents = eventos
      },
      error: error => {
        this.spinner.hide();
        this.toastrService.error('Erro ao carregar os eventos.','Erro!');
      },
      complete: () => this.spinner.hide()
    });
  }

  public filterEvents(filter: string): Evento[] {
    filter = filter.toLocaleLowerCase();
    return this.events.filter(event =>
      event.tema.toLocaleLowerCase().indexOf(filter) !== -1 ||
      event.local.toLocaleLowerCase().indexOf(filter) !== -1
    );
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  confirm(): void {
    this.modalRef?.hide();
    this.toastrService.success('O evento foi excluído com sucesso.','Excluído!');
  }

  decline(): void {
    this.modalRef?.hide();
  }
}
