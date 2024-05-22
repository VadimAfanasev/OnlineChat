import {Button, Heading, Input, Text} from "@chakra-ui/react"

export const WaitingRoom = ({joinChat}) => {
    const onSubmit = (e) => {
        e.PreventDefault();
        joinChat();
    }
    return (
        <form onSubmit={onSubmit} className="max-w-sm w-full bg-white p-8 rounded shadow-lg"> 
            <Heading>Онлайн чат</Heading>
            <div className="mb-4">
                <Text fontSize={"sm"}>Имя пользователя</Text>
                <Input name="userName" placeholder="Введите ваше имя" />
            </div>
            <div className="mb-4">
                <Text fontSize={"sm"}>Название чата</Text>
                <Input name="userName" placeholder="Введите название чата" />
            </div>
            <Button type="submit" colorScheme="blue">
                Присоединиться
            </Button>
        </form>
    )
}