import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-pageTitle',
  templateUrl: './pageTitle.component.html',
  styleUrls: ['./pageTitle.component.scss']
})
export class PageTitleComponent implements OnInit {

  @Input() pageTitle: string = '';
  @Input() iconClass = 'fa fa-user';
  @Input() pageSubTitle = 'Desde 2024';
  @Input() listButton = false;

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  list(): void {
    this.router.navigate([`/${this.pageTitle.toLowerCase()}/lista`])
  }
}
