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

  public title = "Eventos";
  public widthImg = 120;
  public marginImg = 2;
  public showImg = true;
  private listFiltered: string = '';
  private id: number = 0;

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
    this.pagination = { currentPage: 1, itemsPerPage: 2, totalItems: 1 } as Pagination;
    this.getEventos();
  }

  public showImage(): void{
    this.showImg = !this.showImg;
  }

  public getEventos(): void {
    this.spinner.show();

    this.eventoService.getEventos(this.pagination.currentPage, this.pagination.itemsPerPage).subscribe({
      next: (paginatedResult: PaginatedResult<Evento[]>) => {
        this.events = paginatedResult.result;
        this.filteredEvents = this.events;
        this.pagination = paginatedResult.pagination;
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

  openModal(template: TemplateRef<any>, eventoId: number) {
    this.id = eventoId;
    this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
  }

  public pageChanged(event): void {
    this.pagination.currentPage = event.page;
    this.getEventos(); //this.carregarEventos();
  }

  confirm(): void {
    this.modalRef?.hide();
    this.toastrService.success(`O evento ${this.id} foi excluído com sucesso.`,'Excluído!');
  }

  decline(): void {
    this.modalRef?.hide();
  }

  detalheEvento(eventoId: number) : void{
    this.router.navigate([`eventos/detalhe/${eventoId}`]);
  }
}
