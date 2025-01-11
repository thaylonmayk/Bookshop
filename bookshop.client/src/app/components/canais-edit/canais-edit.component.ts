import { Component } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormControl } from '@angular/forms';
import { CanaisService } from '../../../services/canais.service';

@Component({
  selector: 'app-canais-edit',
  standalone: false,

  templateUrl: './canais-edit.component.html',
  styleUrl: './canais-edit.component.css',
})
export class CanaisEditComponent {
  cod: any;
  descricao = new FormControl('');

  constructor(
    private readonly canaisService: CanaisService,
    private readonly router: Router,
    private readonly route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.cod = this.route.snapshot.params['cod'];
    this.getCanalById();
  }

  getCanalById() {
    this.canaisService.getCanalById(this.cod).subscribe({
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
        this.router.navigate(['/canais']);
      },
    });
  }

  editCanal() {
    this.canaisService
      .editCanal({ cod: this.cod, descricao: this.descricao.value })
      .subscribe(() => {
        this.router.navigate(['/canais']);
      });
  }
}
