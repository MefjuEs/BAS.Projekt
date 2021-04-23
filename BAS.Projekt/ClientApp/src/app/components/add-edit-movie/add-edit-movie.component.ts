import { IGenreList } from './../../interfaces/genres/IGenreList';
import { GenresService } from './../../services/genres.service';
import { IInsertMovieCrewDTO } from './../../interfaces/personnel/IInsertMovieCrewDTO';
import { MoviesService } from 'src/app/services/movies.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IMovieDTO } from 'src/app/interfaces/movies/IMovieDTO';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-add-edit-movie',
  templateUrl: './add-edit-movie.component.html',
  styleUrls: ['./add-edit-movie.component.css']
})
export class AddEditMovieComponent implements OnInit {
  public notFound: boolean;
  public isLoading: boolean;
  public editMode: boolean;
  public movie: IMovieDTO;
  public genreList: IGenreList[]
  movieId: number;
  movieTitle = new FormControl('', [Validators.required, Validators.maxLength(100)]);
  movieDescription = new FormControl('', [Validators.maxLength(2000)]);
  movieReleaseYear = new FormControl('', [Validators.required, Validators.min(1900), Validators.max(2300)]);
  movieLengthInMinutes = new FormControl('', [Validators.required, Validators.min(1), Validators.max(1000000)]);
  file: any;
  updatePhoto: boolean;
  selectedCrew: IInsertMovieCrewDTO[];
  selectedGenres = new FormControl();

  constructor(private route: ActivatedRoute,
    private movieService: MoviesService,
    private genreService: GenresService) { 
      this.isLoading = true;
      this.notFound = false;
    }

  ngOnInit(): void {
    this.route.url.subscribe(urlSegments => {
      this.editMode = urlSegments[1].path === 'edit';

      this.genreService.getGenres().subscribe(data => this.genreList = data);
      
      if(this.editMode === true) {
        this.getMovie(this.route.snapshot.params.id)
      }
      else {
        this.movieId = 0;
        this.movieTitle.setValue('');
        this.movieDescription.setValue('');
        this.movieReleaseYear.setValue('');
        this.movieLengthInMinutes.setValue('');
        this.file = null;
        this.updatePhoto = false;
        this.selectedCrew = [];
        this.selectedGenres = new FormControl();
      }

      this.isLoading = false;
    });
  }

  async getMovie(id) {
    try {
      let getMovieDTO = await this.movieService.getMovie(id);
      let genres = getMovieDTO.genres.map(g => g.genreId);
      let crew = getMovieDTO.personnel.map(p => <IInsertMovieCrewDTO>({personnelId: p.personId, filmCrew: p.memberPosition}))

      this.movieId = getMovieDTO.id;
      this.movieTitle.setValue(getMovieDTO.title);
      this.movieDescription.setValue(getMovieDTO.description);
      this.movieReleaseYear.setValue(getMovieDTO.releaseYear);
      this.movieLengthInMinutes.setValue(getMovieDTO.movieLengthInMinutes);
      this.file = null;
      this.updatePhoto = false;
      this.selectedCrew = crew;
      this.selectedGenres.setValue(genres);
    }
    catch(exception) {
      this.notFound = true;
      console.log('gonwo')
    }
  }

  getTitleErrorMessage() {
    console.log(this.selectedGenres)
    if (this.movieTitle.hasError('required')) {
      return 'Tytuł nie może być pusty';
    }

    return this.movieTitle.hasError('maxLength') ? 'Tytuł jest za długi' : '';
  }

  getReleaseYearErrorMessage() {
    if(this.movieReleaseYear.hasError('required')) {
      return 'Rok nie może być pusty'
    }
    if(this.movieReleaseYear.hasError('min')) {
      return 'Rok nie może być mniejszy niż 1900';
    }
    if(this.movieReleaseYear.hasError('max')) {
      return 'Rok nie może być większy niż 2300';
    }
    return '';
  }

  getTimeInMinutesErrorMessage() {
    if(this.movieReleaseYear.hasError('required')) {
      return 'Czas trwania filmu  nie może być pusty'
    }
    if(this.movieReleaseYear.hasError('min')) {
      return 'Czas trwania filmu nie może być mniejszy niż 1 minuta';
    }
    if(this.movieReleaseYear.hasError('max')) {
      return 'Czas trwania filmu nie może być większy niż 1.000.000 minut';
    }
    return '';
  }

  getDescriptionErrorMessage() {
    return this.movieTitle.hasError('maxLength') ? 'Tytuł jest za długi' : '';
  }
}
