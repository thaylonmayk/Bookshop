import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CanalPrecosService } from '../../../services/canal-precos.service';
import { LivrosService } from '../../../services/livros.service';
import { CanaisService } from '../../../services/canais.service';

@Component({
  selector: 'app-canal-precos-edit',
  standalone: false,

  templateUrl: './canal-precos-edit.component.html',
  styleUrl: './canal-precos-edit.component.css',
})
export class CanalPrecosEditComponent {
  cod: any;
  livros: any[] = [];
  canais: any[] = [];
  livroSelecionado = new FormControl();
  canalSelecionado = new FormControl();
  preco = new FormControl();

  constructor(
    private readonly livroService: LivrosService,
    private readonly canaisService: CanaisService,
    private readonly canalPrecosService: CanalPrecosService,
    private readonly router: Router,
    private readonly route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.cod = this.route.snapshot.params['cod'];
    this.getCanalPrecoById();
    this.getLivros();
    this.getCanais();
  }

  getLivros() {
    this.livroService.getLivros().subscribe((livros: any) => {
      this.livros = livros;
    });
  }

  getCanais() {
    this.canaisService.getCanais().subscribe((canais: any) => {
      this.canais = canais;
    });
  }


  getCanalPrecoById() {
    this.canalPrecosService.getCanalPrecosById(this.cod).subscribe({
      next: (response: any) => {
        this.preco.setValue(response.valor);
        this.livroSelecionado.setValue(
          response.codLivro.map((autor: any) => autor.cod)
        );
        this.canalSelecionado.setValue(
          response.codCanal.map((assunto: any) => assunto.cod)
        );
      },
      error: (error) => {
        let errorMessage = error.error.substring(
          error.error.indexOf(':') + 2,
          error.error.indexOf('.\r\n') + 1
        );
        window.alert(errorMessage);
        this.router.navigate(['/canalPrecos']);
      },
    });
  }

  editCanalPreco() {
    const canalPreco = {
      cod: this.cod, 
      valor: this.preco.value,
      codLivro: this.livroSelecionado.value,
      codCanal: this.canalSelecionado.value,
    };

    this.canalPrecosService.editCanalPrecos(canalPreco).subscribe(() => {
      this.router.navigate(['/canalPrecos']);
    });
  }
}
