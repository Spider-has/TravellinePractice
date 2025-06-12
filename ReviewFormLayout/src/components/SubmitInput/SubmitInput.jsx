import './SubmitInput.css';

export const SubmitInput = ({ ref, text }) => {
    return (
        <input
            ref={ref}
            className="review-form-area__submit form-submit"
            type="submit"
            value={text}
            disabled
        />
    );
};
