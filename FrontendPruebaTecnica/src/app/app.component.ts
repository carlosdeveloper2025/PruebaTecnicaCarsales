import { Component } from '@angular/core';
import { EpisodeListComponent } from "./components/episode-list/episode-list.component";

@Component({
  selector: 'app-root',
  imports: [EpisodeListComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'FrontendPruebaTecnica';
}
