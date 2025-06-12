import { useEffect, useRef } from 'react';
import './RangeInput.css';

const RangePoints = [
    {
        colorMod: 'red',
    },
    {
        colorMod: 'orange',
    },
    {
        colorMod: 'orange',
    },
    {
        colorMod: 'yellow',
    },
    {
        colorMod: 'yellow',
    },
];

export const RangeInput = ({ label, setValue }) => {
    const progressRef = useRef(null);
    const rangeInputRef = useRef(null);
    const inputOptionsArea = useRef(null);

    useEffect(() => {
        rangeInputRef.current.addEventListener('input', event => {
            const maxRange = rangeInputRef.current.getAttribute('max');
            const valueNow = event.target.value;
            const percentage = (valueNow / maxRange) * 100;
            rangeInputRef.current.classList = [];
            rangeInputRef.current.classList.add('range-input-area__range-input');

            if (percentage < 25) rangeInputRef.current.classList.add('range-input-area__range-input_angry');
            else if (percentage < 50)
                rangeInputRef.current.classList.add('range-input-area__range-input_frowning');
            else if (percentage < 75)
                rangeInputRef.current.classList.add('range-input-area__range-input_neutral');
            else if (percentage < 100)
                rangeInputRef.current.classList.add('range-input-area__range-input_smiling');
            else rangeInputRef.current.classList.add('range-input-area__range-input_grinning');

            const options = inputOptionsArea.current.querySelectorAll('option');

            let currColor = '';

            if (percentage >= 50) {
                progressRef.current.style.backgroundColor = '#FFC700';
                currColor = '#FFC700';
            } else if (percentage >= 25) {
                progressRef.current.style.backgroundColor = '#FF8311';
                currColor = '#FF8311';
            } else progressRef.current.style.backgroundColor = 'inherit';

            options.forEach(el => {
                const value = el.getAttribute('value');
                if (value > valueNow) el.style.backgroundColor = '#FFF';
                else el.style.backgroundColor = currColor;
            });

            progressRef.current.style.width = `${percentage}%`;
            setValue(Number(valueNow) + 1);
        });
    });

    return (
        <label className="range-input-area">
            <div className="range-input-area__progress-wrapper">
                <div className="track"></div>
                <div ref={progressRef} id="progress" className="progress"></div>
                <div className="range-input-area__input-area">
                    <input
                        ref={rangeInputRef}
                        className="range-input-area__range-input"
                        type="range"
                        name="range-input"
                        min="0"
                        max="4"
                        list="tickmarks"
                    />
                    <datalist
                        ref={inputOptionsArea}
                        className="range-input-area__range-input-markers"
                        id="tickmarks"
                    >
                        {RangePoints.map((el, i) => (
                            <option
                                key={i}
                                value={i}
                                className={`range-input-area__range-point range-input-area__range-point_color-type_${el.colorMod}`}
                            ></option>
                        ))}
                    </datalist>
                </div>
            </div>
            <span>{label}</span>
        </label>
    );
};
