import './TextInput.css';

export const TextInput = ({ labelText, placeholder, setValue }) => {
    return (
        <label className="text-input-area">
            <span>{labelText}</span>
            <input onChange={setValue} required type="text" placeholder={placeholder} />
        </label>
    );
};
