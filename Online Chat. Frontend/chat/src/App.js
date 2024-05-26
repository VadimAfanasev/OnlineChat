import { WaitingRoom } from "./components/WaitingRoom";
import {HubConnectionBuilder} from "@microsoft/signalr";

function App() {
  const joinChat = async (userName, chatRoom) =>{
    var connection = new HubConnectionBuilder()
      .withUrl("http://localhost:29621/chat")
      .withAutomaticReconnect()
      .build();

    connection.on("ReceiveMessage", (userName, message) => {
      console.log(userName);
      console.log(message);
    })

    try{
      await connection.start();
      await connection.invoke("JoinChat", {userName, chatRoom});

      console.log(connection);
    }
    catch(error){
      console.log(error);
    }
  };
  return (
  <div className="min-h-screen flex items-center justify-center bg-gray-100">
    <WaitingRoom joinChat={joinChat}/>
  </div>
  );
}


export default App;
