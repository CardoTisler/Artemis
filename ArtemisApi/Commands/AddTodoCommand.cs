using Common.Dto;

namespace ArtemisApi.Commands;

public class AddTodoCommand
    {
        public TodoDto Payload { get; set; }

        public AddTodoCommand(TodoDto payload)
        {
            Payload = payload;
        }
    }
