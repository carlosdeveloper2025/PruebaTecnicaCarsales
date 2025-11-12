import { Component, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { RickandmortyService } from '../../services/rickandmorty.service';

@Component({
  selector: 'app-episode-list',
  imports: [DatePipe],
  templateUrl: './episode-list.component.html',
  styleUrl: './episode-list.component.css'
})
export class EpisodeListComponent {
  episodes: any[] = [];
  page = signal(1);
  pages = signal(0);

  constructor(private rickandmortyService: RickandmortyService) {
    this.loadEpisodes();
  }

  loadEpisodes() {
    this.rickandmortyService.getEpisodios(this.page()).subscribe(data => {
      this.pages.set(data.info.pages);
      this.episodes = data.results;
    });
  }

  next() {
    if (this.page() < this.pages()) {
      this.page.set(this.page() + 1);
      this.loadEpisodes();
    }
  }

  prev() {
    if (this.page() > 1) {
      this.page.set(this.page() - 1);
      this.loadEpisodes();
    }
  }
}
