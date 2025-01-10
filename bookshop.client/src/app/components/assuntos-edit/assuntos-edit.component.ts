import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormControl } from '@angular/forms';
import { AssuntosService } from '../../../services/assuntos.service';

@Component({
  selector: 'app-assuntos-edit',
  standalone: false,

  templateUrl: './assuntos-edit.component.html',
  styleUrl: './assuntos-edit.component.css',
})
export class AssuntosEditComponent {
  cod: any;
  descricao = new FormControl('');

  constructor(
    private readonly assuntosService: AssuntosService,
    private readonly router: Router,
    private readonly route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.cod = this.route.snapshot.params['cod'];
    this.getAssuntoById();
  }

  getAssuntoById() {
    this.assuntosService.getAssuntoById(this.cod).subscribe({
      next: (response: any) => {
        this.cod = response.cod;
        this.descricao.setValue(response.descricao);
      },
      error: (error) => {
        let errorMessage = error.error.substring(
          error.error.indexOf(':') + 2,
          error.error.indexOf('.\r\n') + 1
        );
        window.alert(errorMessage);
        this.router.navigate(['/assuntos']);
      },
    });
  }

  editAssunto() {
    this.assuntosService
      .editAssunto({ cod: this.cod, descricao: this.descricao.value })
      .subscribe(() => {
        this.router.navigate(['/assuntos']);
      });
  }
}
