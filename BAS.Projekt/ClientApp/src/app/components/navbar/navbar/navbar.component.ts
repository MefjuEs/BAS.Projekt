import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styles: [
  ]
})
export class NavbarComponent implements OnInit {
  isMenuCollapsed = true;

  constructor() { }

  ngOnInit(): void {
  }

}
