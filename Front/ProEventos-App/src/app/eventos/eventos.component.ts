import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss']
})
export class EventosComponent implements OnInit {

  public events: any = [];
  public filteredEvents: any = [];
  public widthImg: number = 120;
  public marginImg: number = 2;
  public showImg: boolean = true;

  private _listFilter: string = '';

  public get listFilter(): string {
    return this._listFilter;
  }

  public set listFilter(value: string) {
    this._listFilter = value;
    this.filteredEvents = this.listFilter ? this.filterEvents(this.listFilter) : this.events;
  }

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
    this.getEventos();
  }

  public showImage(){
    this.showImg = !this.showImg;
  }

  public getEventos(): void {
    this.http.get('https://localhost:5001/api/evento').subscribe(
      response => this.events = this.filteredEvents = response,
      error => console.log(error)
    );
  }

  public filterEvents(filter: string): any {
    filter = filter.toLocaleLowerCase();
    return this.events.filter(
      (event: any) => event.tema.toLocaleLowerCase().indexOf(filter) !== -1 ||
                      event.local.toLocaleLowerCase().indexOf(filter) !== -1
    )
  }
}
