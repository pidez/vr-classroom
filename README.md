# vr-classroom
Sul Branch final-demo è presente la versione del prototipo utilizzabile sul visore, presenta alcune limitazioni rispetto alla versione desktop.
Sul Branch develop-aggiornato è presente la versione del prototipo utilizzabile tramite simulatore sul desktop.
Il progetto è strutturato su 3 scene :

- scena di login : è possibile effetturare login come teacher (inserendo nei campi appositi username "Teacher1" e password "Password1") oppure come studente (nella versione desktop è necessario inserire una stringa non vuota, mentre nel visore non compare la tastiera per scrivere per cui se si prova ad accedere senza credenziali il campo username viene riempito in automatico)

- scena di creazione/login :  1. se si accede come teacher è possibile creare una stanza a cui poi gli altri utenti potranno collegarsi (è presente un bottone apposito                                    nella scena con la scritta "Create Room");
                              2. se si accede come student verrà mostrata una lista di stanze a cui è possibile collegarsi tramite click sul nome della stanza.
                  
 - scena finale : nella scena finale tutti gli utenti vedranno sulla sinistra il pannello della chat di testo dove è possibile scrivere tramite la tastiera (sul visore non siamo riusciti a far comparire la tastiera per cui questa funzionalità è presente solo nella versione desktop). 
Tutti gli utenti che si collegheranno con un dispositivo dotato di microfono e tastiera riusciranno a comunicare fra loro tramite chat vocale senza bisogno di compiere azioni particolari (è sufficiente parlare nel microfono).
Tutti gli utenti si possono muovere liberamente nella stanza (nella versione desktop è possibile muoversi tramite comandi indicati nel file nella repository)
L'utente che si collega con il ruolo di teacher visualizza sulla destra il pulsante di impostazioni che, se cliccato, permette di aprire ad un pannello in cui sono elencati i giocatori connessi ed è presente un pulsante accanto ai nomi degli utenti che permette di attivare/disattivare il microfono dell'utente selezionato dal teacher.
Inoltre, il teacher ha la possibilità di creare delle bandierine su dei pannelli gialli sparsi per la mappa, per farlo deve : attivare il raggio con il primary button del controller del visore o tasto B sulla tastiera (tenere premuto), puntare verso il pannello giallo e premere il pulsante Trigger del controller del visore o tasto sinistro del mouse, per distruggerla deve : avere attivo il raggio, puntare verso la base della bandierina e prendere il Secondaty Button del controller del visore o tasto N della tastiera.

La versione Unity utilizzata per lo sviluppo è la 2021.3.15f1
