using AutoMapper;
using LoanOriginationService.Application.DTO.Customer;
using LoanOriginationService.Application.DTO.LoanDeals;
using LoanOriginationService.Application.DTO.Officer;
using LoanOriginationService.Application.Interfaces;
using LoanOriginationService.Domain.Enum;
using LoanOriginationService.Domain.Models;
using LoanOriginationService.Infrastructure.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanOriginationService.Infrastructure.Repository
{
    public class LoanDealsRepo : IloanDealsRepo
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;
        private readonly OfficerClient officerclient;
        private readonly ILogger<LoanDealsRepo> _logger;
        private readonly CustomerClient customerClient;
        private readonly ScorecardClient scorecardClient;

        public LoanDealsRepo(
            ApplicationDbContext db,
            IMapper mapper,
            CustomerClient customerClient,
            ScorecardClient scorecardClient,
            ILogger<LoanDealsRepo> logger,
            OfficerClient officerclient)
        {
            this.db = db;
            this.mapper = mapper;
            this.customerClient = customerClient;
            this.scorecardClient = scorecardClient;
            this._logger = logger;
            this.officerclient = officerclient;
        }
        public async Task AddLoanDealsAsync(LoanDealsResponseDto dto)
        {
            try
            {
                var loanid = db.LoanTypes.FirstOrDefault(c => c.loanTypeId == dto.loanTypeId);

                if (loanid == null)
                    throw new ArgumentException("Loan Type Not Found");

                if (dto.riskRating < 1 || dto.riskRating > 10)
                    throw new ArgumentException("Risk Rating must be between 1 and 10");

                // 🔥 CREATE ENTITY FIRST
                var d = mapper.Map<LoanDeals>(dto);

                var customer = await customerClient.GetCustomerDetailsById(dto.custId);
                _logger.LogInformation("Customer fetched");

                var scorecard = await scorecardClient
    .GetFullScorecardByCustomerId(dto.custId);
                _logger.LogInformation("Scorecard fetched");

                if (customer == null)
                    throw new ArgumentException("Customer not found");

                if (scorecard == null)
                    throw new ArgumentException("Scorecard not found");

                // 🔥 combine both services
                d.custId = customer.customerId;
                d.scorecardId = scorecard.ScorecardId;
                d.cibilScore = scorecard.cibilScore;
                d.eligibleAmount = scorecard.eligibleLoanAmount;
                d.approvedAmount = scorecard.eligibleLoanAmount * 92 / 100;


                db.LoanDeals.Add(d);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Caught an error in LoanDeals POST");
                throw;
            }
        }

        public async Task ApproveLoanDeal(int id, LoanDecisionDto e)
        {
            // With Client
            var d = await db.LoanDeals
                            .Include(x => x.LoanType)
                            .FirstOrDefaultAsync(x => x.dealId == id);


            if (d == null)
            {
                throw new ArgumentException("Loan Deal does not exist");
            }
            if (d.currentStatus != LoanDealDecisionStatus.Review)
            {
                throw new ArgumentException("Loan Deal is not in Review Stage");
            }
            var officers = await officerclient.GetOfficerDetails();

            if (officers == null)
            {
                throw new Exception("No officer data found");
            }

            var random = new Random();
            var selectedOfficer = officers[random.Next(officers.Count)];

         

            db.DealReviews.Add(new DealReview
            {
                dealId = id,

                officerId = selectedOfficer.id,

                decision = "Approved",

                decisionReason = e.Reason,

                reviewDate = DateTime.Now,
            });

            d.currentStatus = LoanDealDecisionStatus.Approved;
            d.modifiedAt = DateTime.Now;
            await db.SaveChangesAsync();

            // without client

            //  var d = db.LoanDeals.FirstOrDefault(c => c.dealId == id);

            //  if (d == null)
            //  {
            //      throw new ArgumentException("Loan Deal does not exist");
            //  }
            //  if (d.currentStatus != LoanDealDecisionStatus.Review)
            //  {
            //      throw new ArgumentException("Loan Deal is not in Review Stage");
            //  }
            //  var officers = await officerclient.GetOfficerDetails();

            //  if (officers == null)
            //  {
            //      throw new Exception("No officer data found");
            //  }

            //;

            //  db.DealReviews.Add(new DealReview
            //  {
            //      dealId = id,

            //      officerId = e.officerId,

            //      decision = "Approved",

            //      decisionReason = e.Reason,

            //      reviewDate = DateTime.Now,
            //  });

            //  d.currentStatus = LoanDealDecisionStatus.Approved;
            //  d.modifiedAt = DateTime.Now;
            //  db.SaveChanges();


        }

        public async Task RejectLoanDeal(int id, LoanDecisionDto e)
        {
            // with client
            var d = await db.LoanDeals
                            .Include(x => x.LoanType)
                            .FirstOrDefaultAsync(x => x.dealId == id);

            if (d == null)
            {
                throw new Exception("Loan Deal does not exist");
            }
            if (d.currentStatus != LoanDealDecisionStatus.Review)
            {
                throw new Exception("Loan Deal is not in Review Stage");
            }

            var officers = await officerclient.GetOfficerDetails();
            _logger.LogInformation("Officer count: " + officers.Count);

            if (officers == null)
            {
                throw new Exception("No officer data found");
            }

            var random = new Random();
            var selectedOfficer = officers[random.Next(officers.Count)];
         

            db.DealReviews.Add(new DealReview
            {
                dealId = id,

                officerId = selectedOfficer.id,

                decision = "Rejected",

                decisionReason = e.Reason,

                reviewDate = DateTime.Now,
            });

            d.currentStatus = LoanDealDecisionStatus.Rejected;
            d.modifiedAt = DateTime.Now;
            await db.SaveChangesAsync();

            // without client
            //var d =await  db.LoanDeals.FirstOrDefaultAsync(c => c.dealId == id);

            //if (d == null)
            //{
            //    throw new Exception("Loan Deal does not exist");
            //}
            //if (d.currentStatus != LoanDealDecisionStatus.Review)
            //{
            //    throw new Exception("Loan Deal is not in Review Stage");
            //}



            //db.DealReviews.Add(new DealReview
            //{
            //    dealId = id,

            //    //officerId = e.OfficerId,

            //    decision = "Rejected",

            //    decisionReason = e.Reason,

            //    reviewDate = DateTime.Now,
            //});

            //d.currentStatus = LoanDealDecisionStatus.Rejected;
            //d.modifiedAt = DateTime.Now;
            //db.SaveChanges();

        }

        public async Task<List<LoanDealsDto>> GetAllLoanDeals()
        {
            var deals = await db.LoanDeals
                                .Include(x => x.LoanType)
                                .ToListAsync();

            var result = new List<LoanDealsDto>();

            foreach (var d in deals)
            {
                var dto = mapper.Map<LoanDealsDto>(d);

                var customer = await customerClient.GetCustomerDetailsById(d.custId);

                if (customer != null)
                    dto.AuthUserName = customer.AuthUserName;

                result.Add(dto);
            }

            return result;
        }

        public async Task<LoanDealsDto> GetLoanDealsByCustId(int cid)
        {
            var d = await db.LoanDeals.FirstOrDefaultAsync(c => c.custId == cid && c.isActive);
            var res = mapper.Map<LoanDealsDto>(d);
            return res;
        }

        public async Task<LoanDealsDto> GetLoanDealsById(int id)
        {
            var d = await db.LoanDeals
                            .Include(x => x.LoanType)
                            .FirstOrDefaultAsync(x => x.dealId == id);

            if (d == null)
                return null;

            var res = mapper.Map<LoanDealsDto>(d);

            // 🔥 Get name from ScoreCard customer endpoint
            var scorecardCustomer =
                await scorecardClient.GetScorecardCustomerById(d.custId);

            if (scorecardCustomer != null)
                res.AuthUserName = scorecardCustomer.AuthUserName;

            return res;
        }


    }
}
