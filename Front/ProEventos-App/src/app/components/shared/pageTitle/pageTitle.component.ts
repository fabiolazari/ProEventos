import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-pageTitle',
  templateUrl: './pageTitle.component.html',
  styleUrls: ['./pageTitle.component.scss']
})
export class PageTitleComponent implements OnInit {

  @Input() pageTitle: string = '';

  constructor() { }

  ngOnInit(): void {
  }

}
