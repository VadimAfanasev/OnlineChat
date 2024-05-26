import { CloseButton, Heading } from "@chakra-ui/react";

export const Chat = ({messages, chatRoom, closeChat}) => {
    return (
        <div className = "w-1/2 bg-white p-8 rounded shadow-lg">
            <div className="flex flex-row justify-between mb-5">
                <Heading size="lg">{chatRoom}</Heading>
                <CloseButton onClick={closeChat} />
            </div>
            <div className="flex flex-col overflow-auto scroll-smooth h-96 gap-3 pb-3">
                {messages.map(messageInfo, index => {
                    <Message/>
                })}
            </div>
        </div>
    );
}