
using FlightAppLibrary.Models;
using FlightAppLibrary.Models.Dtos;
using FlightAppLibrary.Models.Interfaces;

namespace FlightAppFrontend.Shared.Extensions
{
    public static class CommentExtensions
    {
        public static DisplayComment ToDisplayComment(this IDisplayComment displayComment)
        {
            string flightIata = "";
            string text = "";

            if (displayComment is NoteDto noteDto)
            {
                flightIata = noteDto.FlightIata;
                text = noteDto.NoteText;
            }
            if(displayComment is ReplyDto replyDto)
            {
                flightIata = replyDto.Note!.FlightIata;
                text = replyDto.ReplyText;
            }

            return new DisplayComment()
            {
                FlightIata = flightIata,
                Text = text,
                TimeStamp = displayComment.TimeStamp,
            };
        }
    }
}
