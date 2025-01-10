import { Component } from '@angular/core';

@Component({
  selector: 'app-header',
  standalone: false,
  
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent {
  visible: boolean = true;
	staticAlertClosed = false;
  
  constructor() {
    setTimeout(() => this.staticAlertClosed = true, 20000);
  }

  close() {
    this.visible = false;
  }
  
  redirectToGithub() {
    window.location.href = 'https://github.com/thaylonmayk/Bookshop';
  }

}