import type { CardsDomData, Coords } from '../../model';
import { DropZone } from '../DropZone/DropZone';
import { HorizontalDivider } from '../HorizontalDivider/HorizontalDivider';
import { TaskCard } from '../TaskCard/TaskCard';
import './Column.css';

type ColumnProps = {
    columnName: string;
    columnCards: CardsDomData[];
    setCardRef: (cardI: number, el: HTMLDivElement | null) => void;
    setColumnRef: (el: HTMLElement | null) => void;
    setHighestDragZoneRef: (el: HTMLDivElement | null) => void;
    onMouseUp: (currElem: CardsDomData, coords: Coords) => void;
    onMouseMove: (currElem: CardsDomData, coords: Coords) => void;
    setDraggedProcess: (val: boolean) => void;
};

export const Column = (props: ColumnProps) => {
    return (
        <section ref={el => props.setColumnRef(el)} className="week-area">
            <h2>{props.columnName}</h2>
            <HorizontalDivider />
            <div className="week-area__tasks-area">
                <DropZone ref={el => props.setHighestDragZoneRef(el)} />
                {props.columnCards.map((el, i) => (
                    <TaskCard
                        onMouseMove={props.onMouseMove}
                        setDraggedProcess={props.setDraggedProcess}
                        onMouseUp={props.onMouseUp}
                        setCardRef={(elem: HTMLDivElement | null) => {
                            props.setCardRef(i, elem);
                        }}
                        key={el.id}
                        {...el}
                    />
                ))}
            </div>
        </section>
    );
};
