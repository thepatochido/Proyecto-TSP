    using UnityEngine;

    public class ColeccionablesScript : MonoBehaviour
    {
   
        public float interactionRadius = 0.2f;  // Radio de interacción para hacer clic del jugador
        public AudioClip collectSound;  // Sonido al recoger el coleccionable

        public bool isCollected = false; //Inicia falso, se hace verdadero para desaparecerlo
        private bool recogido = false;

        // Update is called once per frame
        void Update()
        {
            transform.localRotation = Quaternion.Euler(0,Time.time*100f,0); //Gira cada ciero tiempo en el eje y, para crear el movimiento

            // Verifica si el jugador hace clic derecho en el coleccionable y está dentro del radio de interacción, y que tampoco se haya recogido
            if (Input.GetMouseButtonDown(0) && IsPlayerNearby() && !recogido)
            {
                Collect();
            }
        }

        void OnTriggerEnter(Collider other) 
        {
            // Verifica si el jugador entra en contacto con el coleccionable y si aún no se recolecta
            if (other.CompareTag("Player") && !recogido)
            {
                Collect();
            }
        }

        public bool IsCollected()
        {
        return recogido; //Regresa el booleano para usarlo en el controlador
        }

        void Collect()
        {
            if (!isCollected)
            {
            
                 Debug.Log("Coleccionable recogido desde script: " + gameObject.name); //Pruebas
                // Reproduce el sonido de recoger
                if (collectSound != null)
                {
                    AudioSource.PlayClipAtPoint(collectSound, transform.position); //Suena el sonido de recoleccion
                }

                // Desactiva el objeto para que no se pueda recoger nuevamente
                gameObject.SetActive(false);

                recogido = true; //Hace verdadero para dejar de usar el objeto después de destruirlo

            }
        }

        bool IsPlayerNearby()
        {
            // Verifica si el jugador está dentro del radio de interacción
            Vector3 playerPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            playerPosition.z = transform.position.z; // Ajusta la posición Z para que sea la misma que la del coleccionable
            bool isNearby=Vector3.Distance(playerPosition, transform.position) <= interactionRadius;
            Debug.Log("¿Jugador cerca? " + isNearby); //Pruebas

            return isNearby; //Si es menor regresa True, y se recolecta
        }
    }
