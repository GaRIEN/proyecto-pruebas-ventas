document.addEventListener('DOMContentLoaded', function () {
    const loginForm = document.querySelector('form');

    loginForm.addEventListener('submit', async function (e) {
        // 1. Evitamos que el formulario se envíe de forma tradicional (recarga de página)
        e.preventDefault();

        // 2. Capturamos los valores
        const usuario = document.getElementById('Usuario').value;
        const password = document.getElementById('Password').value;

        // 3. Creamos el objeto siguiendo la estructura de tu UsuarioCommand
        // Asegúrate de que las propiedades coincidan con tu clase C#
        const loginData = {
            NombreUsuario: usuario,
            Password: password
        };

        try {
            // 4. Enviamos los datos al controlador de la WebApp
            const response = await fetch('/Account/GetLoginUser', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(loginData)
            });

            // 5. Procesamos la respuesta (ApiResult)
            const result = await response.json();

            if (response.ok && result.isSuccess) {
                // Si el login fue exitoso en la API y en el controlador
                // Redirigimos al Home o Dashboard
                /* window.location.href = '/Home/Index';*/
               console.log(result)
            } else {
                // Mostramos el mensaje de error que viene de tu ApiResult
                alert("Error: " + (result.error || "Credenciales incorrectas"));
            }

        } catch (error) {
            console.error("Error en la petición:", error);
            alert("Ocurrió un error al intentar conectar con el servidor.");
        }
    });
});