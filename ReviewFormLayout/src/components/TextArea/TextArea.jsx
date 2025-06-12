import './TextArea.css';

export const TextArea = ({ placeholder, setValue }) => {
    return (
        <textarea
            name="text-field"
            onChange={setValue}
            className="form-text-area"
            placeholder={placeholder}
        ></textarea>
    );
};
