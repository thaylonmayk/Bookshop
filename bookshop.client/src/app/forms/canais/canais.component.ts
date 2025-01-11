import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CanaisService } from '../../../services/canais.service';

@Component({
  selector: 'app-canais',
  standalone: false,

  templateUrl: './canais.component.html',
  styleUrl: './canais.component.css',
})
export class CanaisComponent {
  canais: any[] = [];

  constructor(
    private readonly canaisService: CanaisService,
    private readonly router: Router
  ) {}

  ngOnInit() {
    this.getCanais();
  }

  getCanais() {
    this.canaisService.getCanais().subscribe((response: any) => {
      this.canais = response;
    });
  }

  addCanal() {
    this.router.navigate(['/canaisAdd']);
  }

  deleteCanal(cod: number) {
    this.canaisService.deleteCanal(cod).subscribe({
      next: () => {
        this.getCanais();
      },
      error: (error) => {
        window.alert(error.error);
      },
    });
  }
}
