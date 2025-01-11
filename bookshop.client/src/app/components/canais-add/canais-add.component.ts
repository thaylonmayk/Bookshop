import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { CanaisService } from '../../../services/canais.service';

@Component({
  selector: 'app-canais-add',
  standalone: false,

  templateUrl: './canais-add.component.html',
  styleUrl: './canais-add.component.css',
})
export class CanaisAddComponent {
  nome = new FormControl('');

  constructor(
    private readonly canaisService: CanaisService,
    private readonly router: Router
  ) {}

  addCanal() {
    this.canaisService
      .addCanal({ nome: this.nome.value })
      .subscribe({
        next: () => {
          this.router.navigate(['/canais']);
        },
        error: (error) => {
          let key = Object.keys(error.error.errors)[0];
          let value = error.error.errors[key][0];
          window.alert(value);
        },
      });
  }
}
