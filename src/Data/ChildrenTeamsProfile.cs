using System.Text.Json.Nodes;
using AutoMapper;
using Prometheus.Data.Entities;
using Prometheus.Models.Announcement;
using Prometheus.Models.Child;
using Prometheus.Models.Match;
using Prometheus.Models.Notifications;
using Prometheus.Models.Payment;
using Prometheus.Models.Team;
using Prometheus.Models.User;

namespace Prometheus.Data;

public class ChildrenTeamsProfile : Profile {
  public ChildrenTeamsProfile() {
    CreateMap<User, BasicUserResponse>();
    CreateMap<User, UserResponse>()
      .ForMember(x => x.Roles, expression => expression.MapFrom(u => u.Roles.Select(r => r.Slug)))
      .ForMember(x => x.TotalChildren, expression => expression.MapFrom(u => u.Children.Count(c => !c.Deleted)))
      .ForMember(x => x.TotalPayments, expression => expression.MapFrom(u => u.Payments.Count(c => !c.Deleted)));
    CreateMap<Child, ChildResponse>();
    CreateMap<Announcement, AnnouncementResponse>();
    CreateMap<Match, MatchResponse>();
    CreateMap<Payment, PaymentResponse>()
      .ForMember(
        p => p.IssuedAt,
        builder => builder.MapFrom(p => p.CreatedAt));
    CreateMap<Team, BasicTeamResponse>();
    CreateMap<Team, TeamResponse>()
      .ForMember(x => x.TotalMembers, expression => expression.MapFrom(t => t.Members.Count(c => !c.Deleted)))
      .ForMember(x => x.TotalMatches, expression => {
        expression.MapFrom(t => t.FirstTeamMatches.Count(c => !c.Deleted) + t.SecondTeamMatches.Count(c => !c.Deleted));
      });

    CreateMap<Notification, NotificationResponse>();
  }
}
