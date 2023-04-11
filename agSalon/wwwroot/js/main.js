const deletes = document.querySelectorAll('.delete');

const deletes_a = document.querySelectorAll('.delete-confirm');

if (deletes_a.length > 0) {
    for (let a of deletes_a) {
        a.addEventListener('click', function () {

            let parentA = a.parentElement;

            for (let del of deletes) {
                if (del != parentA)
                    del.classList.remove('active');
            }

            parentA.classList.toggle('active');
        });
    }
}


document.onclick = function (e) {
    for (let del of deletes)
        if (!e.composedPath().includes(del)) {
            del.classList.remove('active');
        }
}

document.onkeydown = function (e) {
    if (e.keyCode === 27)
        for (let del of deletes)
            if (!e.composedPath().includes(del))
                del.classList.remove('active');
}
