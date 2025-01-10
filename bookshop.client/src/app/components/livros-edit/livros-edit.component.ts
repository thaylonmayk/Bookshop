import { Component } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AutoresService } from '../../../services/autores.service';
import { AssuntosService } from '../../../services/assuntos.service';
import { LivrosService } from '../../../services/livros.service';
import { LivroRequestDto } from '../../dto/livro/livro';

@Component({
  selector: 'app-livros-edit',
  standalone: false,

  templateUrl: './livros-edit.component.html',
  styleUrl: './livros-edit.component.css',
})
export class LivrosEditComponent {
  cod: any;

  autores: any[] = [];
  assuntos: any[] = [];

  autoresSelecionados = new FormControl();
  assuntosSelecionados = new FormControl();
  titulo = new FormControl();
  editora = new FormControl();
  edicao = new FormControl();
  sinopse = new FormControl();
  anoPublicacao = new FormControl();
  valor = new FormControl();

  constructor(
    private readonly autoresService: AutoresService,
    private readonly assuntosService: AssuntosService,
    private readonly livrosService: LivrosService,
    private readonly router: Router,
    private readonly route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.cod = this.route.snapshot.params['cod'];
    this.getLivroById();
    this.getAutores();
    this.getAssuntos();
  }

  getAutores() {
    this.autoresService.getAutores().subscribe((autores: any) => {
      this.autores = autores;
    });
  }

  getAssuntos() {
    this.assuntosService.getAssuntos().subscribe((assuntos: any) => {
      this.assuntos = assuntos;
    });
  }

  getLivroById() {
    this.livrosService.getLivroById(this.cod).subscribe({
      next: (response: any) => {
        this.titulo.setValue(response.titulo);
        this.editora.setValue(response.editora);
        this.edicao.setValue(response.edicao);
        this.anoPublicacao.setValue(response.anoPublicacao);
        this.sinopse.setValue(response.sinopse);
        this.autoresSelecionados.setValue(
          response.autores.map((autor: any) => autor.cod)
        );
        this.assuntosSelecionados.setValue(
          response.assuntos.map((assunto: any) => assunto.cod)
        );
      },
      error: (error) => {
        let errorMessage = error.error.substring(
          error.error.indexOf(':') + 2,
          error.error.indexOf('.\r\n') + 1
        );
        window.alert(errorMessage);
        this.router.navigate(['/livros']);
      },
    });
  }

  editLivro() {
    const livro: LivroRequestDto = {
      cod: this.cod,
      titulo: this.titulo.value,
      editora: this.editora.value,
      edicao: this.edicao.value,
      sinopse: this.sinopse.value,
      anoPublicacao: this.anoPublicacao.value,
      autores: this.autoresSelecionados.value,
      assuntos: this.assuntosSelecionados.value,
    };

    this.livrosService.editLivro(livro).subscribe(() => {
      this.router.navigate(['/livros']);
    });
  }
}
