import { memo, useCallback, useRef } from 'react';
import type { CardsDomData, Coords } from '../../model';
import { DropZone } from '../DropZone/DropZone';
import './TaskCard.css';

interface TaskCardProps extends CardsDomData {
    setCardRef: (el: HTMLDivElement | null) => void;
    onMouseUp: (currElem: CardsDomData, coords: Coords) => void;
    onMouseMove: (currElem: CardsDomData, coords: Coords) => void;
    setDraggedProcess: (val: boolean) => void;
}

export const TaskCard = memo((props: TaskCardProps) => {
    const cardRef = useRef<HTMLDivElement>(null);
    const mouseOffset = useRef<Coords>(null);
    const backgroundRef = useRef<HTMLDivElement>(null);
    const dragZone = useRef<HTMLDivElement>(null);

    const onMouseMove = useCallback(
        (ev: MouseEvent) => {
            if (cardRef.current && mouseOffset.current) {
                const top = ev.pageY - mouseOffset.current.y;
                const left = ev.pageX - mouseOffset.current.x;
                cardRef.current.style.top = `${top}px`;
                cardRef.current.style.left = `${left}px`;

                props.onMouseMove({ ...props }, { x: left, y: top });
            }
        },
        [props],
    );

    const onMouseUp = useCallback(() => {
        if (cardRef.current) {
            const coords = cardRef.current.getBoundingClientRect();
            props.onMouseUp({ ...props }, { x: coords.left, y: coords.top });

            cardRef.current.style.position = 'static';
            cardRef.current.style.top = '';
            cardRef.current.style.left = '';

            props.setDraggedProcess(false);

            document.removeEventListener('mousemove', onMouseMove);
            document.removeEventListener('mouseup', onMouseUp);
        }
        if (backgroundRef.current) backgroundRef.current.style.display = 'none';
    }, [onMouseMove, props]);

    const onMouseDown = useCallback(
        (event: React.MouseEvent<HTMLDivElement, MouseEvent>) => {
            if (cardRef.current) {
                cardRef.current.style.position = 'absolute';

                const CurrPos = cardRef.current.getBoundingClientRect();

                mouseOffset.current = {
                    y: event.pageY - CurrPos.top,
                    x: event.pageX - CurrPos.left,
                };

                cardRef.current.style.top = `${event.pageY - mouseOffset.current.y}px`;
                cardRef.current.style.left = `${event.pageX - mouseOffset.current.x}px`;

                props.setDraggedProcess(true);

                document.addEventListener('mousemove', onMouseMove);
                document.addEventListener('mouseup', onMouseUp);
            }
            if (backgroundRef.current) backgroundRef.current.style.display = 'block';
        },
        [onMouseMove, onMouseUp, props],
    );

    return (
        <div className="task-card-interaction-area" ref={el => props.setCardRef(el)}>
            <div ref={backgroundRef} className={`task-card task-card_dnd-background`}>
                {props.taskText}
            </div>
            <div onMouseDown={onMouseDown} ref={cardRef} className="task-card">
                {props.taskText}
            </div>
            <DropZone ref={dragZone} text={props.taskText} />
        </div>
    );
});
