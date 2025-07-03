import { Component, OnInit, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';
import { Pagination, PaginatedResult } from '@app/models/pagination';

@Component({
  selector: 'app-evento-lista',
  templateUrl: './evento-lista.component.html',
  styleUrls: ['./evento-lista.component.scss']
})
export class EventoListaComponent implements OnInit {

  modalRef?: BsModalRef;
  public events: Evento[] = [];
  public filteredEvents: Evento[] = [];
  public pagination = {} as Pagination;
  public eventId: number = 0;

  public title = "Eventos";
  public widthImg = 120;
  public marginImg = 2;
  public showImg = true;

  private listFiltered: string = '';

  constructor(
      private eventoService: EventoService,
      private modalService: BsModalService,
      private toastrService: ToastrService,
      private spinner: NgxSpinnerService,
      private router: Router
    ) { }

  public get listFilter(): string {
    return this.listFiltered;
  }

  public set listFilter(value: string) {
    this.listFiltered = value;
    this.filteredEvents = this.listFilter ? this.filterEvents(this.listFilter) : this.events;
  }

  public ngOnInit(): void {
    this.pagination = { currentPage: 0, itemsPerPage: 10, totalItems: 10 } as Pagination;
    this.carregarEventos();
  }

  public showImage(): void{
    this.showImg = !this.showImg;
  }

  public carregarEventos(): void {
    this.spinner.show();

    this.eventoService.getEventos(this.pagination.currentPage, this.pagination.itemsPerPage).subscribe(
      (paginatedResult: PaginatedResult<Evento[]>) => {
        this.events = paginatedResult.result;
        this.filteredEvents = this.events;
        this.pagination = paginatedResult.pagination;
      },
      (error) => {
        this.spinner.hide();
        this.toastrService.error('Erro ao carregar os eventos.','Erro!');
        console.log(error);
      },
      () => this.spinner.hide()
    );
  }

  public filterEvents(filter: string): Evento[] {
    filter = filter.toLocaleLowerCase();
    return this.events.filter(event =>
      event.tema.toLocaleLowerCase().indexOf(filter) !== -1 ||
      event.local.toLocaleLowerCase().indexOf(filter) !== -1
    );
  }

  openModal(event: any, template: TemplateRef<any>, eventoId: number) {
    event.stopPropagation();
    this.eventId = eventoId;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  public pageChanged(event): void {
    this.pagination.currentPage = event.page;
    this.carregarEventos();
  }

  confirm(): void {
    this.modalRef?.hide();
    this.spinner.show();

    this.eventoService.deleteEvent(this.eventId).subscribe(
      (result: any) => {
        console.log(result);
        this.toastrService.success(`O evento ${this.eventId} foi excluído com sucesso.`,'Excluído!');
        this.pagination = { currentPage: 0, itemsPerPage: 10, totalItems: 10 } as Pagination;//ver depois aqui como proceder...
        this.carregarEventos();
      },
      (error: any) => {
        this.toastrService.error('Erro ao deletar o evento.','Erro!');
        console.log(error);
      }
    ).add(() => this.spinner.hide());
  }

  decline(): void {
    this.modalRef?.hide();
  }

  detalheEvento(eventoId: number) : void{
    this.router.navigate([`eventos/detalhe/${eventoId}`]);
  }
}
