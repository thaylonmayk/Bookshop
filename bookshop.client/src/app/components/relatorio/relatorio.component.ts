import { Component } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { DownloadRelatorioService } from '../../../services/download-relatorio.service';

@Component({
  selector: 'app-relatorio',
  templateUrl: './relatorio.component.html',
  styleUrls: ['./relatorio.component.css'],
  standalone: false,
})
export class RelatorioComponent {

  constructor(private readonly downloadService: DownloadRelatorioService, private toastr: ToastrService) { }

  ngOnInit() { }

  downloadPdf() {
    this.toastr.info('Aguarde o download do relatório.', 'Download Iniciado');
    this.downloadService.getPdf()
      .subscribe((response) => {
        this.downloadFile(response, 'application/pdf', 'relatorio', 'pdf');
      });
  }

  downloadCsv() {
    this.toastr.info('Aguarde o download do relatório.', 'Download Iniciado');
    this.downloadService.getCsv()
      .subscribe((response) => {
        this.downloadFile(response, 'text/csv', 'relatorio', 'csv');
      });
  }

  downloadExcel() {
    this.toastr.info('Aguarde o download do relatório.', 'Download Iniciado');
    this.downloadService.getExcel()
      .subscribe((response) => {
        this.downloadFile(response, 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet', 'relatorio', 'xlsx');
      });
  }

  private downloadFile(data: BlobPart, type: string, filename: string, extension: string) {
    const blob = new Blob([data], { type });
    const url = URL.createObjectURL(blob);
    const anchor = document.createElement('a');
    const date = new Date();
    const dateString = `${date.getFullYear()}${(date.getMonth() + 1).toString().padStart(2, '0')}${date.getDate().toString().padStart(2, '0')}_${date.getHours().toString().padStart(2, '0')}${date.getMinutes().toString().padStart(2, '0')}${date.getSeconds().toString().padStart(2, '0')}`;
    anchor.href = url;
    anchor.download = `${filename}_${dateString}.${extension}`;
    document.body.appendChild(anchor);
    anchor.click();
    document.body.removeChild(anchor);
    URL.revokeObjectURL(url);
  }
}
