import { Component } from '@angular/core';
import { AutoresService } from '../../../services/autores.service';
import { Autor } from './autor';
import { Router } from '@angular/router';

@Component({
  selector: 'app-autores',
  standalone: false,

  templateUrl: './autores.component.html',
  styleUrl: './autores.component.css',
})
export class AutoresComponent {
  autores: Autor[] = [];

  constructor(
    private readonly autoresService: AutoresService,
    private readonly router: Router
  ) {}

  ngOnInit() {
    this.getAautores();
  }

  getAautores() {
    this.autoresService.getAutores().subscribe((response) => {
      this.autores = response;
    });
  }

  addAutor() {
    this.router.navigate(['/autoresAdd']);
  }

  deleteAutor(cod: number) {
    this.autoresService.deleteAutor(cod).subscribe({
      next: () => {
        this.getAautores();
      },
      error: (error) => {
        window.alert(error.error);
      },
    });
  }
}
