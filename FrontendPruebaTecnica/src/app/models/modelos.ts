export interface episodio {
    name: string;
    air_date: string;
    episode: string;
    characters: string[];
    url: string;
    created: string;
}

export interface Info {
    count: number;
    pages: number;
    next: string | null;
    prev: string | null;
}

export interface PagedResponse<T> {
    info: Info;
    results: T[];
}