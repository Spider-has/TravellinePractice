const create = (oldObj, newObj) => {
    const oldKeys = Object.keys(oldObj);
    const newKeys = Object.keys(newObj);
    const allKeys = [...new Set([...oldKeys, ...newKeys]).values()];
    
    const getType = (oldVal, newVal) => {
        if(oldVal === undefined)
            return 'new'
        else if(newVal === undefined)
            return 'deleted'
        else return oldVal === newVal?  'equal' : 'change'
    }
    
    const result = allKeys.map((el) => {
        const oldVal = oldObj[el];
        const newVal = newObj[el];
        return {
            type: getType(oldVal, newVal),
            oldVal, newVal,
            el
        }
    })

    const reduced = result.reduce((accumulator, {el, ...item}) => ({
        ...accumulator, 
        [el]: {...item}
    }), {})

    return new Promise((resolve, reject) => {
        setTimeout(() => {
            resolve(reduced);
        }, 500)
    })
}


export const JsonComparator = { create }