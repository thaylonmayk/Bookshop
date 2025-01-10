import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { LivrosService } from '../../../services/livros.service';

@Component({
  selector: 'app-livros',
  standalone: false,

  templateUrl: './livros.component.html',
  styleUrl: './livros.component.css',
})
export class LivrosComponent {
  livros: any[] = [];

  constructor(
    private readonly livrosService: LivrosService,
    private readonly router: Router
  ) {}

  ngOnInit() {
    this.getLivros();
  }

  getLivros() {
    this.livrosService.getLivros().subscribe((response: any) => {
      this.livros = response;
      this.livros.forEach((livro) => {
        livro.assuntos = livro.assuntos
          .map((livroAssunto: any) => livroAssunto.assunto.descricao)
          .join(', ');
        livro.autores = livro.autores
          .map((livroAutor: any) => livroAutor.autor.nome)
          .join(', ');
      });
    });
  }

  addLivro() {
    this.router.navigate(['/livrosAdd']);
  }

  deleteLivro(cod: number) {
    this.livrosService.deleteLivro(cod).subscribe(() => {
      this.getLivros();
    });
  }
}
