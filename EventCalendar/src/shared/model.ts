export interface CardData {
    id: number;
    taskText: string;
}

export interface CardsDomData extends CardData {
    cardDivRef: HTMLDivElement | null;
}

export type Coords = {
    x: number;
    y: number;
};

export type ColumnCardsData = {
    cards: CardsDomData[];
    columnRef: HTMLElement | null;
    hightestDropZone: HTMLDivElement | null;
};
